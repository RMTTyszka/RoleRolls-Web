package com.loh.authentication;

import org.springframework.security.authentication.AbstractAuthenticationToken;
import org.springframework.security.core.GrantedAuthority;

import java.util.Collection;

public class LohUserAuthenticationToken extends AbstractAuthenticationToken {
    private LohUserDetails principal;
    private String credential;

    /**
     * Creates a token with the supplied array of authorities.
     *
     * @param authorities the collection of <tt>GrantedAuthority</tt>s for the principal
     *                    represented by this authentication object.
     */
    public LohUserAuthenticationToken(LohUserDetails user, String password, Collection<? extends GrantedAuthority> authorities) {
        super(authorities);
        this.principal = user;
        this.credential = password;
        super.setAuthenticated(true); // must use super, as we override
    }
    public LohUserAuthenticationToken(LohUserDetails user, String password) {
        super(null);
        this.principal = user;
        this.credential = password;
        super.setAuthenticated(false); // must use super, as we override
    }

    @Override
    public String getCredentials() {
        return this.credential;
    }

    @Override
    public LohUserDetails getPrincipal() {
        return this.principal;
    }

    public void setAuthenticated(boolean isAuthenticated) throws IllegalArgumentException {
        if (isAuthenticated) {
            throw new IllegalArgumentException(
                    "Cannot set this token to trusted - use constructor which takes a GrantedAuthority list instead");
        }

        super.setAuthenticated(false);
    }
}
