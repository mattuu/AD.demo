export class Person {
    id: number;
    firstName: string;
    lastName: string;
    isValid: boolean;
    isAuthorised: boolean;
    isEnabled: boolean;
    colours: Colour[];
}

export class Colour {
    id: number;
    name: string;
    isEnabled: boolean;
    selected: boolean;
}