package com.loh.application.roles;

import com.fasterxml.jackson.core.JsonProcessingException;
import com.loh.domain.roles.Role;
import com.loh.domain.roles.RoleRepository;
import com.loh.domain.universes.UniverseType;
import com.loh.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.data.domain.Page;
import org.springframework.data.domain.PageRequest;
import org.springframework.data.domain.Pageable;
import org.springframework.data.jpa.domain.Specification;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.*;

import static org.springframework.data.jpa.domain.Specification.where;


@CrossOrigin
@Controller    // This means that this class is a Controller
@RequestMapping(path="/roles",  produces = "application/json; charset=UTF-8")
public class RoleController extends BaseCrudController<Role, Role, Role, RoleRepository> {

	@Autowired
	RoleRepository repository;

	public RoleController(RoleRepository repository) {
		super(repository);
		this.repository = repository;
	}

	@Override
	public Role getNew() {
		return new Role();
	}

	@Override
	@GetMapping()
	public @ResponseBody
	Page<Role> getList(@RequestParam String filter, @RequestParam int skipCount, @RequestParam int maxResultCount, @RequestHeader("universe-type") UniverseType universeType) throws JsonProcessingException {
		Pageable paged = PageRequest.of(skipCount, maxResultCount);
		return repository.findAll(where(fromUniverse(universeType)), paged);
	}
	@Override
	protected Role createInputToEntity(Role role) {
		return role;
	}

	@Override
	protected Role updateInputToEntity(Role role) {
		return role;
	}

	static Specification<Role> fromUniverse(UniverseType universeType) {
		return (role, cq, cb) -> cb.equal(role.get("universeType"), universeType);
	}
}
