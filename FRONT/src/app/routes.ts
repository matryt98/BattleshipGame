import { RouteProps } from "react-router-dom"
import game from "modules/Game"

const routes: RouteProps[] = [
  // Add routes below
  ...game.routes,
]

export default routes