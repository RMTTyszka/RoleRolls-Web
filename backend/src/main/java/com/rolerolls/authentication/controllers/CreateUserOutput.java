package com.rolerolls.authentication.controllers;

public class CreateUserOutput {
    public boolean success;
    public boolean userNameOrEmailPreviouslyRegistered;
    public boolean invalidPassword;

    public CreateUserOutput() {
        success = true;
    }
}
