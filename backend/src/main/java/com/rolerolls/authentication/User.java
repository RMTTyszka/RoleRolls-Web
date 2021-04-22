package com.rolerolls.authentication;

import com.rolerolls.shared.DefaultEntity;
import lombok.Getter;
import lombok.Setter;

import javax.persistence.Lob;
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
    private String userName;
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
    @Lob
    private String password;

}
