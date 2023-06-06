import { Box } from "@mui/material"
import { Notification } from "../../features"
import { useLocation } from "react-router-dom";
import { Pagination } from "../../components";
import { green } from "@mui/material/colors";

const HomePage = () => {
	const location = useLocation()
	const message = location.state && location.state.message
	const type = location.state && location.state.type

	return (
		<Box bgcolor={green[900]} minHeight='100vh' >
			{message && <Notification message={message} type={type} />}
			<Pagination apiUrl={process.env.REACT_APP_PRODUCT_API ?? ''} />
		</Box>
	)
}

export default HomePage