
import {CatagoryServiceClient } from "./Protos/category_grpc_web_pb";
import {Empty} from "./Protos/category_pb";
import React, { useState, useEffect } from 'react';

import { Button } from 'react-bootstrap';
import Category from './Category';
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
  
  return (
    <div className="App">
      <Category></Category>
      
    </div>
  );
}

export default App;
