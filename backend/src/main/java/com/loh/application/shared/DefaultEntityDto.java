package com.loh.application.shared;

import com.loh.shared.EntityDto;
import lombok.Getter;
import lombok.Setter;

public class DefaultEntityDto extends EntityDto {
    @Getter
    @Setter
    protected boolean systemDefault;
}
