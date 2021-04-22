package com.rolerolls.application.shared;

import com.rolerolls.shared.EntityDto;
import lombok.Getter;
import lombok.Setter;

public class DefaultEntityDto extends EntityDto {
    @Getter
    @Setter
    protected boolean systemDefault;
}
