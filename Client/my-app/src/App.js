import './App.css';
import {CatagoryServiceClient } from "./Protos/category_grpc_web_pb";
import {Empty} from "./Protos/category_pb";
import React, { useState, useEffect } from 'react';

import { Button } from 'react-bootstrap';
const categoryClient = new CatagoryServiceClient ("https://localhost:7173", null, null);
function App() {
  const [Categories, setCategories] = useState([]);
  const [buttonAdd, setButtonAdd] = useState(true);
  useEffect(() => {
    
    const onGetAll = () => {
      var empty =  new Empty ();
      categoryClient.getAll( empty,null, (err, res) => {
        setCategories(res.toObject().itemsList);
      });
    }
    onGetAll();
    
  },[]);
  const onButton = () => {
    console.log(Categories);
  }
  return (
    <div className="App">
      <Button onClick={onButton} variant="primary">Add</Button>
    <table className="styled-table">
    <thead>
    <tr>
          <th>Name</th>
          <th>address</th>
    </tr>
    </thead>
    <tbody>
    {
        Categories.map((item,i)=>{
          return (
            <tr key ={i}>
            <td>{item.name}</td>
            <td colSpan={2}>{item.tagname}</td>
            <td><button >detail</button></td>
          </tr>)
          
        })
      }
    </tbody>
</table>
      
    </div>
  );
}

export default App;
