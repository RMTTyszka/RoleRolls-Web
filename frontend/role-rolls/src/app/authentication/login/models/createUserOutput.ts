export interface CreateUserOutput {
  success: boolean;
  userNameOrEmailPreviouslyRegistered: boolean;
  invalidPassword: boolean;
}
