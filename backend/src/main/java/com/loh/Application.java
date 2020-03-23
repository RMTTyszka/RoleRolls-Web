package com.loh;

import com.loh.adventures.Encounter;
import com.loh.adventures.EncounterRepository;
import com.loh.adventures.Enviroment;
import com.loh.adventures.EnviromentRepository;
import com.loh.context.PlayerRepository;
import com.loh.items.armors.armorModel.ArmorModel;
import com.loh.items.armors.armorModel.ArmorModelRepository;
import com.loh.items.equipable.weapons.weaponModel.WeaponModel;
import com.loh.items.equipable.weapons.weaponInstance.WeaponInstanceRepository;
import com.loh.powers.Power;
import com.loh.powers.PowerInstance;
import com.loh.powers.PowerRepository;
import com.loh.race.Race;
import com.loh.race.RaceRepository;
import com.loh.role.Role;
import com.loh.role.RoleRepository;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.CommandLineRunner;
import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.context.annotation.Bean;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.ResponseBody;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.EntityTransaction;
import java.lang.reflect.Field;
import java.lang.reflect.ParameterizedType;
import java.math.BigInteger;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import java.util.UUID;

@Controller
@SpringBootApplication
public class Application {

	@RequestMapping("/")
	@ResponseBody
	String home() {
		return "worked";
	}

	@Autowired
	RoleRepository roleRepo;
	@Autowired
	EnviromentRepository enviromentRepo;
	@Autowired
	RaceRepository raceRepo;
	@Autowired
	ArmorModelRepository armorRepo;
	@Autowired
	WeaponInstanceRepository weaponRepo;
	@Autowired
	EncounterRepository encounterRepo;
	@Autowired
	PowerRepository powerRepo;
	
	@Autowired
	EntityManagerFactory entityManagerFactorty;

	public <T> T createAndFill(Class<T> clazz) throws Exception {
		T instance = clazz.newInstance();
	for (Field field : clazz.getDeclaredFields()) {
	if (field.getName() == "id" && field.getType() != UUID.class) {
	continue;
	}

	field.setAccessible(true);
	Object value = getRandomValueForField(field);
	field.set(instance, value);
	}
	if (clazz.getSuperclass() != null) {
	for (Field field : clazz.getSuperclass().getDeclaredFields()) {
	if (field.getName() == "id") {
	continue;
	}
	field.setAccessible(true);
	Object value = getRandomValueForField(field);
	field.set(instance, value);
	}
	}
	return instance;
	}

	private Object getRandomValueForField(Field field) throws Exception {

		Class<?> type = field.getType();

		// Note that we must handle the different types here! This is just an
		// example, so this list is not complete! Adapt this to your needs!
		if (type.isEnum()) {
			Object[] enumValues = type.getEnumConstants();
			return enumValues[random.nextInt(enumValues.length)];
		} else if (type.equals(Integer.TYPE) || type.equals(Integer.class)) {
			return 18;
			//return random.nextInt(20);
		} else if (type.equals(Long.TYPE) || type.equals(Long.class)) {
			return random.nextLong();
		} else if (type.equals(Double.TYPE) || type.equals(Double.class)) {
			return random.nextDouble();
		} else if (type.equals(Float.TYPE) || type.equals(Float.class)) {
			return random.nextFloat();
		} else if (type.equals(String.class)) {
			return "strength";
		} else if (type.equals(UUID.class)) {
			return UUID.randomUUID();
		} else if (type.equals(BigInteger.class)) {
			return BigInteger.valueOf(random.nextInt());
		} else if(type.equals(List.class)) {
			ParameterizedType newType = (ParameterizedType) field.getGenericType();
			Class<?> newClass = (Class<?>) newType.getActualTypeArguments()[0];
			List<Object> newList = new ArrayList<>();
			if (newClass.getName() == "com.loh.shared.Bonus" || newClass.getName() == "com.loh.powers.Power" ) {
				newList.add(createAndFill(newClass));
				return newList;
			} else if (newClass.getName() == "java.lang.String") {
				String string = UUID.randomUUID().toString().substring(30);
				newList.add(string);
				return newList;
			}
			EntityManager manager = entityManagerFactorty.createEntityManager();
			EntityTransaction entityTransaction = manager.getTransaction();
			entityTransaction.begin();
			//ArrayList newList = (ArrayList) field.getType().getClass().
			for (int x = 0; x < 2; x++) {
				Object obj = createAndFill(newClass); 
				manager.persist(obj);
				newList.add(obj);
			}
			
			entityTransaction.commit();
			manager.close();
			return newList;
		} else if (type.equals(Role.class)) {	
			Role role = createAndFill(Role.class);
			roleRepo.save(role);
			return role;
		} else if (type.equals(WeaponModel.class)) {
/*			WeaponModel weapon = createAndFill(WeaponModel.class);
			weaponRepo.save(weapon);
			return weapon;*/
		}else if (type.equals(ArmorModel.class)) {
			ArmorModel armor = createAndFill(ArmorModel.class);
			armorRepo.save(armor );
			return armor ;
		}
		else if (type.equals(Enviroment.class)) {
			Enviroment enviroment = createAndFill(Enviroment.class);
			enviromentRepo.save(enviroment);
			System.out.println(enviroment.getName());
			return enviroment;
		} else if (type.equals(Race.class)) {
			Race race = createAndFill(Race.class);
			raceRepo.save(race);
			return race;
		} else if (type.equals(PowerInstance.class)) {
			PowerInstance power = createAndFill(PowerInstance.class);
			return power;
		} else if (type.equals(Power.class)){
			Power power = createAndFill(Power.class);
			powerRepo.save(power);
			return power;
		}
		return createAndFill(type);
	}

	private Random random = new Random();
	// private EnhancedRandom randomB = EnhancedRandomBuilder.aNewEnhancedRandom();

	public static void main(String[] args) {
		SpringApplication.run(Application.class, args);
	}

	@Bean
	public CommandLineRunner demoData(PlayerRepository player, RaceRepository race,
									  RoleRepository roleRepo, EnviromentRepository enviromentRepo, ArmorModelRepository armorRepo, PowerRepository powerRepo) {
		return args -> {

			if (false) {

				long armorCount = armorRepo.count();
				if (armorCount <= 0) {
					for (int x = 0; x < 10; x++) {
						ArmorModel armor = createAndFill(ArmorModel.class);
						armorRepo.save(armor);
					}
				}
				for (int x = 0; x < 20; x++) {
				}
				for (int x = 0; x < 2; x++) {
					Encounter encounter = createAndFill(Encounter.class);
					encounterRepo.save(encounter);
				}

			}

		};
	}
	public <T> T ifClassExist(Class<?> clazz, T obj) throws Exception {
		for (Field field : clazz.getDeclaredFields()) 
		{
			if (field.getName() == "id" && field.getType() != UUID.class) {
				continue;
			}
			field.setAccessible(true);
			Object value = getRandomValueForField(field);
			field.set(obj, value);
		}
		if (clazz.getSuperclass() != null) 
		{
			return ifClassExist(clazz.getSuperclass(), obj);
		}
		 else {
			return obj;
		}
	}
}
