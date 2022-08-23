import { Cells, Coordinates } from "types/interfaces"
import axiosInstance from "./axios"

/**
 * This class is used for communication with API
 * 
 * Sample usage:
 * 
 * 	public async getSomeData(request: RequestType) {
 * 		return await this.axios.get<ResponseType>(
 * 			'url_to_the_endpoint',
 * 			request
 * 		)
 * 	}
 */

class Api {
	private axios = axiosInstance

	// add api request methods below
	
	public async startNewGame() {
		return await this.axios.get<string>(
			'Game/StartNew'
		)
	}

	public async getGameCells(guid: string) {
		const params = new URLSearchParams()
		params.append('gameId', guid)

		return await this.axios.get<Cells>(
			`Game/GetGameCells?${params}`
		)
	}

	public async drawShips(guid: string) {
		const params = new URLSearchParams()
		params.append('gameId', guid)

		return await this.axios.post<Cells>(
			`Game/DrawShips?${params}`
		)
	}

	public async attack(guid: string, coordinates: Coordinates) {
		const params = new URLSearchParams()
		params.append('gameId', guid)

		return await this.axios.post<boolean>(
			`Game/Attack?${params}`,
			coordinates
		)
	}

	public async opponentAttack(guid: string) {
		const params = new URLSearchParams()
		params.append('gameId', guid)

		return await this.axios.post<Coordinates>(
			`Game/OpponentAttack?${params}`
		)
	}
}

export default Api