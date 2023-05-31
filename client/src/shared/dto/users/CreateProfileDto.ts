interface CreateProfileDto{
	firstName: string;
	middleName: string;
	lastName: string;
	phoneNumber: string;
	userImage: string;
	birthDate: string;
	gender: "male" | "female";
}

export interface ChangeUserDto{
	firstName: string;
	middleName: string;
	lastName: string;
	phoneNumber: string;
	userImage: string;
	birthDate: string;
	identityId: string;
	isActive: boolean;
	gender: "male" | "female";
}

export default CreateProfileDto