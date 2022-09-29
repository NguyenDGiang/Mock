import './App.css';
import {CatagoryServiceClient } from "./Protos/category_grpc_web_pb";
import {Empty,PagingCategoryRequest, CategoryProto,CategoryRowIdFilter} from "./Protos/category_pb";
import React, { useState, useEffect } from 'react';
import Table from 'react-bootstrap/Table';
import { Button } from 'react-bootstrap';
import moment from 'moment';
import ReactPaginate from 'react-paginate';
import Form from 'react-bootstrap/Form';
import Modal from 'react-bootstrap/Modal';
const categoryClient = new CatagoryServiceClient ("https://localhost:7173", null, null);
function Category() {
  const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);
  const [Categories, setCategories] = useState([]);
  const [buttonAdd, setButtonAdd] = useState(false);
  const [buttonPut, setButtonPut] = useState(false);
  const [pageCount, setPageCount] = useState(0);
  const [pageCurrent, setPageCurrent] = useState(1);
  const [Category, setCategory] = useState({
    Name : "",
    TagName: "",
    Active: false
  });
  const [Name, setName] = useState("");
  const [TagName, setTagName] = useState("");
  const [Active, setActive] = useState(true);
  const [CategoryId, setCategoryId] = useState(false);
  const [Message, setMessage] = useState("");
  const onGetAll = (index = 1) => {
      
    var pagingCategoryRequest =  new PagingCategoryRequest ();
    pagingCategoryRequest.setPageindex(index);
    pagingCategoryRequest.setPagesize(5);
    categoryClient.getPaging(pagingCategoryRequest,null, (err, res) => {
      setPageCount(Math.ceil(res.toObject().count / pagingCategoryRequest.getPagesize));
      setCategories(res.toObject().dataList);
    });
  }
  const onAdd = () => {
    console.log("hello");
    var categoryProto =  new CategoryProto ();
    categoryProto.setName(Name);
    categoryProto.setActive(Active);
    categoryProto.setTagname(TagName);
    categoryClient.insert(categoryProto,null, (err, res) => {
      setMessage(res.toObject().status)
      alert(res.toObject().status);
    });
    
  }
  const onPut = () => {
    console.log(Active);
    var categoryProto =  new CategoryProto ();
    categoryProto.setName(Name);
    categoryProto.setActive(Active);
    categoryProto.setTagname(TagName);
    categoryProto.setId(CategoryId);
    categoryClient.put(categoryProto,null, (err, res) => {
      setMessage(res.toObject().status)
      alert(res.toObject().status);
    });
    
  }
  const onDelete = (item) => {
    console.log(Active);
    var categoryProto =  new CategoryRowIdFilter ();
    categoryProto.setId(item.id);
    
    categoryClient.delete(categoryProto,null, (err, res) => {
      setMessage(res.toObject().status)
      alert(res.toObject().status);
    });
    
  }
  useEffect(() => {
    
   
    onGetAll();
    
  
  },[]);
  const onFormSubmit = e => {
    e.preventDefault();
    // send state to server with e.g. `window.fetch`
  }
  
  const handlePageClick = (event) => {
    setPageCurrent(event.selected + 1);
    onGetAll(event.selected + 1);
    console.log(event.selected +1);
  };
  const onButton = () => {
    setButtonAdd(!buttonAdd);
  }
  const onButtonPut = (item) => {
    setName(item.name);
    setTagName(item.tagname);
    setActive(item.active);
    setCategoryId(item.id);
    setButtonPut(!buttonPut);
  }
  const onClose = () => {
    setButtonPut(false);
  }
  function getFormattedDate(date) {
    let year = date.getFullYear();
    let month = (1 + date.getMonth()).toString().padStart(2, '0');
    let day = date.getDate().toString().padStart(2, '0');
  
    return day + '/' +  month + '/' + year;
  }
  return (
    <div className="App">
        
        <Button variant="primary" onClick={handleShow}>
        Add
      </Button>

      <Modal show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title>Add</Modal.Title>
        </Modal.Header>
        <Modal.Body>
        <Form onSubmit={onFormSubmit}>
      <Form.Group className="mb-3" >
        <Form.Label>Name</Form.Label>
        <Form.Control onChange={e =>setName(e.target.value)}  type="text" placeholder="Enter Text" />
      </Form.Group>

      <Form.Group className="mb-3" controlId="formBasicPassword">
        <Form.Label>Tag Name</Form.Label>
        <Form.Control onChange={e =>setTagName(e.target.value)}  type="text" placeholder="Tag Name" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="formBasicCheckbox">
        <Form.Label>Active</Form.Label>
        <Form.Check onChange={e =>setActive(e.target.checked)}  type="checkbox"/>
      </Form.Group>
      
      </Form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick = {()=>onAdd()} >
            Save Changes
          </Button>
        </Modal.Footer>
      </Modal>
      {

        buttonPut == true &&
        <Form onSubmit={onFormSubmit}>
      <Form.Group className="mb-3" >
        <Form.Label>Name</Form.Label>
        <Form.Control onChange={e =>setName(e.target.value)} value ={Name}   type="text" placeholder="Enter Text" />
      </Form.Group>

      <Form.Group className="mb-3" controlId="formBasicPassword">
        <Form.Label>Tag Name</Form.Label>
        <Form.Control onChange={e =>setTagName(e.target.value)} value ={TagName}  type="text" placeholder="Tag Name" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="formBasicCheckbox">
        <Form.Label>Active</Form.Label>
        <Form.Check onChange={e =>setActive(e.target.checked)} checked={Active}  type="checkbox"/>
      </Form.Group>
      <Button onClick = {()=>onPut()} variant="primary" type="submit">
        Submit
      </Button>
      <Button onClick = {()=>onClose()} variant="primary" type="submit">
        Close
      </Button>
      </Form>
      }
      
      <Table striped bordered hover size="sm">
      <thead>
        <tr>
          <th>Name</th>
          <th>Tag Name</th>
          <th>Active</th>
          <th>Ngày Tạo</th>
        </tr>
      </thead>
      <tbody>
      {
        Categories.map((item,i)=>{
          console.log((new Date(item.createddate.seconds * 1000)).getFullYear())
          return (
            <tr key ={i}>
            <td>{item.name}</td>
            <td>{item.tagname}</td>
            <td>{item.active == 1 ? "Active" : "No Active"}</td>
            <td>{getFormattedDate(new Date(item.createddate.seconds * 1000))}</td>
            <td><Button onClick = {()=>onButtonPut(item)} variant="secondary" size="sm">
          Edit
        </Button>
        <Button onClick = {()=>onDelete(item)} variant="secondary" size="sm">
          Delete
        </Button>
        
        </td>
          </tr>)
        })
      }
       
      </tbody>
    </Table>
    
    <ReactPaginate
          previousLabel={"prev"}
          nextLabel={"next"}
          breakLabel={"..."}
          breakClassName={"break-me"}
          pageCount={15}
          onPageChange={handlePageClick}
          containerClassName={"pagination"}
          subContainerClassName={"pages pagination"}
          activeClassName={"active"}
        />
    </div>
  );
}

export default Category;
