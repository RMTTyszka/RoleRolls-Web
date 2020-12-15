package com.loh;

import com.loh.application.creatures.heroes.HeroController;
import org.junit.Test;
import org.junit.runner.RunWith;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.test.context.junit4.SpringRunner;

import static org.assertj.core.api.Assertions.assertThat;

@RunWith(SpringRunner.class)
@SpringBootTest
public class ApplicationTests {

	@Autowired
	private HeroController heroController;
	@Test
	public void contextLoads() {
		assertThat(heroController).isNotNull();
	}

}
