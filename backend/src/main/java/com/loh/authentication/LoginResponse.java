package com.loh.authentication;

import lombok.Getter;
import lombok.Setter;

public class LoginResponse {
    @Getter
    @Setter
    private String token;

    public LoginResponse(String token) {
        this.token = token;
    }
}
