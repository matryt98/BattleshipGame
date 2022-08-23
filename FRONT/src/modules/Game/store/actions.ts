import { createAsyncThunk } from "@reduxjs/toolkit"
import { Api } from "api"
import { Coordinates } from "types/interfaces"

const api = new Api()

interface AttackPayload {
	gameId: string
	coordinates: Coordinates
}

export const startNewGame = createAsyncThunk(
	'game/startNewGame',
	async() => {
		const response = await api.startNewGame()
		return response.data
	}
)

export const getGameCells = createAsyncThunk(
	'game/getGameCells',
	async(payload: string) => {
		const response = await api.getGameCells(payload)
		return response.data
	}
)

export const drawShips = createAsyncThunk(
	'game/drawShips',
	async(payload: string) => {
		const response = await api.drawShips(payload)
		return response.data
	}
)

export const attack = createAsyncThunk(
	'game/attack',
	async(payload: AttackPayload) => {
		const response = await api.attack(payload.gameId, payload.coordinates)
		return response.data
	}
)

export const opponentAttack = createAsyncThunk(
	'game/opponentAttack',
	async(payload: string) => {
		const response = await api.opponentAttack(payload)
		return response.data
	}
)