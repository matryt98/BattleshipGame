import { useAppDispatch } from "app/hooks"
import { actions } from "modules/Game/store"
import { Button } from "modules/Shared/components"
import { useEffect, useMemo, useState } from "react"
import { Link, useLocation } from "react-router-dom"
import { CellType } from "types/enums"
import { Cell, Cells, Coordinates } from "types/interfaces"
import './PlayGame.css'

const getEmptyBoard = (): Cells => {
	return {
		friendlyCells: Array.from(Array(100).keys()).map(i => {
			const cell: Cell = {
				hit: false,
				coordinates: {
					row: Math.floor(i / 10) + 1,
					col: (i % 10) + 1 
				},
				type: CellType.Water,
			}
	
			return cell
		}),
		opponentCells: Array.from(Array(100).keys()).map(i => {
			const cell: Cell = {
				hit: false,
				coordinates: {
					row: Math.floor(i / 10) + 1,
					col: (i % 10) + 1 
				},
				type: CellType.Water,
			}
	
			return cell
		}),
	}

}

const PlayGame = () => {
	const dispatch = useAppDispatch()
	const location = useLocation()

	const [gameId, setGameId] = useState<string>('')
	const [cells, setCells] = useState<Cells>(getEmptyBoard())
	const [attackingDisabled, setAttackingDisabled] = useState(true)
	const [isGameStarted, setIsGameStarted] = useState(false)

	useEffect(() => {
		const idFromUrl = location.pathname.split("/").pop()

		if(idFromUrl) {
			dispatch(actions.getGameCells(idFromUrl))
			.unwrap()
			.then(cells => {
				if(cells.opponentCells.some(x => x.hit)) {
					setIsGameStarted(true)
					setAttackingDisabled(false)
				}
				else {
					setIsGameStarted(false)
					setAttackingDisabled(true)
				}
				setCells(cells)
			})
	
			setGameId(idFromUrl)
		}
	
	}, [location, dispatch])

	const handleDrawShips = () => {
		dispatch(actions.drawShips(gameId))
		.unwrap()
		.then(cells => {
			setCells(cells)
			setAttackingDisabled(false)
		})
	}

	const handleAttack = (friendlyCoordinates: Coordinates) => {
		if(!isGameStarted) setIsGameStarted(true)
		setAttackingDisabled(true)
		
		dispatch(actions.attack({
			gameId: gameId,
			coordinates: friendlyCoordinates,
		}))
		.unwrap()
		.then(hit => {
			const opponentIndex = cells.opponentCells.findIndex(x => x.coordinates.row === friendlyCoordinates.row && x.coordinates.col === friendlyCoordinates.col)
			let opponentCells = [...cells.opponentCells]
			opponentCells[opponentIndex].hit = true;

			if(hit) {
				setAttackingDisabled(false)
			}
			else {
				setTimeout(() => handleOpponentAttack(), 500)
			}

			setCells({
				friendlyCells: [...cells.friendlyCells],
				opponentCells: opponentCells,
			})
		})
	}

	const handleOpponentAttack = () => {
		dispatch(actions.opponentAttack(gameId))
		.unwrap()
		.then(coordinates => {
			const friendlyIndex = cells.friendlyCells.findIndex(x => x.coordinates.row === coordinates.row && x.coordinates.col === coordinates.col)
			let friendlyCells = [...cells.friendlyCells]
			friendlyCells[friendlyIndex].hit = true;

			if(friendlyCells[friendlyIndex].type === CellType.Water) {
				setAttackingDisabled(false)
			}
			else {
				setTimeout(() => handleOpponentAttack(), 500)
			}

			setCells({
				friendlyCells: friendlyCells,
				opponentCells: [...cells.opponentCells],
			})
		})
	}

	const IamWinner = useMemo(
		() => cells.opponentCells.filter(x => x.hit && x.type === CellType.Ship).length === 17,
		[cells]
	)

	const IamLoser = useMemo(
		() => cells.friendlyCells.filter(x => x.hit && x.type === CellType.Ship).length === 17,
		[cells]
	)

	return (
		<div style={{ padding: 10, textAlign: 'center' }}>
			<div className={IamWinner || IamLoser ? 'backdrop' : 'hidden'}/>
			{(IamWinner || IamLoser) && (
				<div className="notification">
					<h1>Game over!</h1>
					<h3>{IamWinner ? "You won!!!" : "You lost!"}</h3>
					<Link to="/">Home</Link>
				</div>
			)}
			<Link to="/">Home</Link>
			<h1>Play Game</h1>
			<br/>
			<Button onClick={handleDrawShips} disabled={!gameId || isGameStarted}>Draw Ships</Button>
			<div className="flex">
				<div className="grid-container">
					{cells.friendlyCells.map((cell, index) => (
						<div
							key={index}
							style={{ backgroundColor: cell.type === CellType.Water ? 'initial' : 'lightblue'}}
							className="grid-item"
						>
							{cell.hit && (
								cell.type === CellType.Ship
								? <span style={{ color: 'red', fontWeight: 'bold' }}>X</span>
								: <span>*</span>
							)}
						</div>
					))}
				</div>
				<div className="grid-container">
					{cells.opponentCells.map((cell, index) => (
						<div
							key={index}
							style={{ backgroundColor: cell.type === CellType.Water || !cell.hit ? 'initial' : 'lightblue'}} 
							className={['grid-item', (cell.hit || attackingDisabled) ? 'disabled' : 'clickable'].join(' ')}
							onClick={() => handleAttack(cell.coordinates)}
						>
							{cell.hit && (
								cell.type === CellType.Ship
								? <span style={{ color: 'red', fontWeight: 'bold' }}>X</span>
								: <span>*</span>
							)}
						</div>
					))}
				</div>
			</div>
		</div>
	)
}

export default PlayGame