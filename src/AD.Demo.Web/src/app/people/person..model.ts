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
}

export interface ISelectable<T> {
    item: T;
    selected: boolean;
}

export class Selectable<T> implements ISelectable<T> {
    constructor(obj: T, selected: boolean) {
        this.item = obj;
        this.selected = selected;
    }

    item: T;

    selected: boolean;
}

export class UpdatePerson {
    firstName: string;
    lastName: string;
    isValid: boolean;
    isAuthorised: boolean;
    isEnabled: boolean;
    colourIds: number[];

    constructor(person: Person, colours: number[]) {
        this.firstName = person.firstName;
        this.lastName = person.lastName;
        this.isValid = person.isValid;
        this.isAuthorised = person.isAuthorised;
        this.isEnabled = person.isEnabled;
        this.colourIds = colours;
    }
}