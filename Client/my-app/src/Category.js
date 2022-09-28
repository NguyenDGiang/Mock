import './App.css';
import {CatagoryServiceClient } from "./Protos/category_grpc_web_pb";
import {Empty,PagingCategoryRequest} from "./Protos/category_pb";
import React, { useState, useEffect } from 'react';
import Table from 'react-bootstrap/Table';
import { Button } from 'react-bootstrap';
import moment from 'moment';
import ReactPaginate from 'react-paginate';
import Form from 'react-bootstrap/Form';
const categoryClient = new CatagoryServiceClient ("https://localhost:7173", null, null);
function Category() {
  const [Categories, setCategories] = useState([]);
  const [buttonAdd, setButtonAdd] = useState(false);
  const [pageCount, setPageCount] = useState(0);
  const [pageCurrent, setPageCurrent] = useState(1);
  const onGetAll = (index = 1) => {
      
    var pagingCategoryRequest =  new PagingCategoryRequest ();
    pagingCategoryRequest.setPageindex(index);
    pagingCategoryRequest.setPagesize(3);
    categoryClient.getPaging(pagingCategoryRequest,null, (err, res) => {
      setPageCount(Math.ceil(res.toObject().count / pagingCategoryRequest.getPagesize));
      setCategories(res.toObject().dataList);
    });
  }
  useEffect(() => {
    
   
    onGetAll();
    
  
  },[]);
  const handlePageClick = (event) => {
    setPageCurrent(event.selected + 1);
    onGetAll(event.selected + 1);
    console.log(event.selected +1);
  };
  const onButton = () => {
    setButtonAdd(!buttonAdd);
  }
  
  return (
    <div className="App">
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
      <Button onClick={onButton} variant="primary">Add</Button>
      {
        buttonAdd == true &&
        <Form>
      <Form.Group className="mb-3" controlId="formBasicEmail">
        <Form.Label>Email address</Form.Label>
        <Form.Control type="email" placeholder="Enter email" />
        <Form.Text className="text-muted">
          We'll never share your email with anyone else.
        </Form.Text>
      </Form.Group>

      <Form.Group className="mb-3" controlId="formBasicPassword">
        <Form.Label>Password</Form.Label>
        <Form.Control type="password" placeholder="Password" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="formBasicCheckbox">
        <Form.Check type="checkbox" label="Check me out" />
      </Form.Group>
      <Button variant="primary" type="submit">
        Submit
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
          return (
            <tr key ={i}>
            <td>{item.name}</td>
            <td>{item.tagname}</td>
            <td>{moment(item.createddate.seconds).format("DD-MM-YYYY")}</td>
            <td><button >detail</button></td>
          </tr>)
        })
      }
       
      </tbody>
    </Table>
    
    </div>
  );
}

export default Category;
