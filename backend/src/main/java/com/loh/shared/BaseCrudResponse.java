package com.loh.shared;

public class BaseCrudResponse<T extends Entity> {
    public Boolean success;
    public String message;
    public T entity;

    public BaseCrudResponse() {
    }

    public BaseCrudResponse(Boolean success, String message, T entity) {
        this.success = success;
        this.message = message;
        this.entity = entity;
    }
    public BaseCrudResponse(Boolean success, String message) {
        this.success = success;
        this.message = message;
        entity = null;
    }
}
