package com.loh.authentication;

import com.loh.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.validation.constraints.NotEmpty;
import javax.validation.constraints.NotNull;

@javax.persistence.Entity
public class User extends DefaultEntity {
    @Getter
    @Setter
    @NotNull
    @NotEmpty
    private String firstName;
    @Getter
    @Setter
    @NotNull
    @NotEmpty
    private String lastName;
    @Getter
    @Setter
    @NotNull
    @NotEmpty
    @ValidEmail
    private String email;
    @Getter
    @Setter
    @NotNull
    @NotEmpty
    private String password;
    @Getter
    @Setter
    @NotNull
    @NotEmpty
    private String salt;

}
