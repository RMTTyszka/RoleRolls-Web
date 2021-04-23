package com.rolerolls.shared;

import lombok.Getter;

public class ValidationResult<TOutput, TErrorType extends Enum<TErrorType>> {
    public boolean isSuccess() {
        return this.errorType == null;
    };
    @Getter
    private TErrorType errorType;
    @Getter
    private TOutput output;


    public ValidationResult(TOutput output, TErrorType errorType) {
        this.errorType = errorType;
        this.output = output;
    }
}
