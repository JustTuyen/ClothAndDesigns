import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { createBrowserRouter, RouterProvider } from 'react-router'
import './index.css'

//
import NotFound from './view/NotFound.jsx'
import DetailPage from './view/DetailPage.jsx'
import Home from './view/homePage.jsx'

const router = createBrowserRouter([
  {path:'/*', element:<NotFound/>},
  {path:'/', element:<Home/>},
  {path:'/product', element:<DetailPage/>},
])

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <RouterProvider router={router}/>
  </StrictMode>,
)
