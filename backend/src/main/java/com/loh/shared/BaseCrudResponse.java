package com.loh.shared;

import lombok.Getter;
import lombok.Setter;

public class BaseCrudResponse<T extends Entity> {
    @Getter @Setter
    public Boolean Success;
    @Getter @Setter
    public String Message;
    @Getter @Setter
    public T Entity;

}
