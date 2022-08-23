import "./Button.css"

const Button: React.FC<React.DetailedHTMLProps<React.ButtonHTMLAttributes<HTMLButtonElement>, HTMLButtonElement>> = (props) => (
	<button
		{...props}
		className={['button', props.className].join('')}
	/>
)

export default Button