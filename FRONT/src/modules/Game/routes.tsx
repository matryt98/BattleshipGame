import { RouteProps } from "react-router-dom"
import Home from "./pages/Home"
import PlayGame from "./pages/PlayGame"

const routes: RouteProps[] = [
  {
    path: '/',
    element: <Home/>
  },
  {
    path: '/PlayGame/:gameId',
    element: <PlayGame/>
  },
]



export default routes
