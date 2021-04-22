package com.rolerolls.application.creatures.heroes;

import com.rolerolls.application.creatures.heroes.dtos.HeroDto;
import com.rolerolls.application.creatures.mappers.HeroMapper;
import com.rolerolls.domain.creatures.heroes.Hero;
import com.rolerolls.shared.MapperBuilder;
import com.rolerolls.utils.TestUtils;
import org.junit.Test;
import org.springframework.boot.test.context.SpringBootTest;

import static org.assertj.core.api.Assertions.assertThat;

@SpringBootTest
public class HeroMapperTests {
    @Test
    public void TestHeroMapper(){
        Hero hero = new Hero();
        hero.setName(TestUtils.names[0]);
        HeroMapper mapper = new MapperBuilder<HeroMapper>().build(HeroMapper.class);
        HeroDto heroDto = mapper.map(hero);
        assertThat(heroDto.name).isEqualTo(hero.getName());
    }
}
