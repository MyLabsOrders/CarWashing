import { Box } from "@mui/material";
import { RegisterForm } from "../../components";
import { green } from "@mui/material/colors";

const Register = () => {
	return (
		<Box bgcolor={green[900]} minHeight="100vh">
			<RegisterForm />
		</Box>
	)
}

export default Register;