export default class UserProfile {
    public userName: string;
    public email: string;
    public firstName: string;
    public lastName: string;
    public photoURL: string = '/assets/images/avatars/avatar_25.jpg';
    public role: string;

    public get displayName() {
        return `${this.firstName} ${this.lastName}`;
    }
}