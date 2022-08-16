import { RouteProps } from "react-router-dom"

export interface Module<Reducer> {
  routes: RouteProps[]
  reducer: Reducer
}