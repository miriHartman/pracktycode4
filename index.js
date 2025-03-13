import express from 'express'
import renderApi from '@api/render-api';

const app = express()
const port = 3001



  app.get('',(req,res)=>{



    renderApi.auth('rnd_D7d39EUu7B8Wz5iNdd1YCL6pah1S');
    renderApi.listServices({includePreviews: 'true', limit: '20'})
      .then(({ data }) => res.send(data))
      .catch(err => console.error(err));
      
  }


);  


 app.listen(port,() => {
    console.log(`my application is listening on http://localhost:${port}`);}) 