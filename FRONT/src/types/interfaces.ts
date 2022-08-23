import { RouteProps } from "react-router-dom"
import { CellType } from "./enums"

export interface Module<Reducer> {
  routes: RouteProps[]
  reducer: Reducer
}

export interface Cell {
  type: CellType
  coordinates: Coordinates
  hit: boolean
}

export interface Coordinates {
  row: number
  col: number
}

export interface Cells {
  friendlyCells: Cell[]
  opponentCells: Cell[]
}