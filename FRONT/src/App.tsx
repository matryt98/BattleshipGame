import routes from "app/routes"
import { store } from "app/store"
import React from "react"
import { Provider } from "react-redux"
import { BrowserRouter, Route, Routes } from "react-router-dom"

const App = () => {
	return (
		<Provider store={store}>
      <BrowserRouter>
        {/* <Loader /> */}
        <Routes>
          {routes.map((route, index) => 
            <Route key={index} {...route} />
          )}
        </Routes>
      </BrowserRouter>
		</Provider>
	)
}

export default App
