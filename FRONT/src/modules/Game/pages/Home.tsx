import { useAppDispatch } from "app/hooks"
import { actions } from "modules/Game/store"
import { Button } from "modules/Shared/components"
import { useState } from "react"
import { Link } from "react-router-dom"

const PlayGame = () => {
	const dispatch = useAppDispatch()

	const [gameId, setGameId] = useState<string>('')

	const handleStartNewGame = () => {
		dispatch(actions.startNewGame())
		.unwrap()
		.then(gameId => setGameId(gameId))
	}

	return (
		<div style={{ padding: 10, textAlign: 'center' }}>
			<h1>Welcome in battleship</h1>
			<Button onClick={handleStartNewGame}>Start new Game</Button>
			{gameId && (
				<div style={{ marginTop: 10 }}>
					<b>Click on the link below to play the game: </b>
					<br/>
					<Link to={`/PlayGame/${gameId}`}>Click me!</Link>
				</div>
			)}
		</div>
	)
}

export default PlayGame