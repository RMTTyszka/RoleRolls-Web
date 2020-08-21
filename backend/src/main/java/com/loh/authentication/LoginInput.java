package com.loh.authentication;

import lombok.Getter;
import lombok.Setter;

public class LoginInput {
    @Getter
    @Setter
    private String email;
    @Getter
    @Setter
    private String password;
}
