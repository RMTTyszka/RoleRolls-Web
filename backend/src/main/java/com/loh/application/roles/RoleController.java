package com.loh.application.roles;

import com.loh.domain.roles.Role;
import com.loh.domain.roles.RoleRepository;
import com.loh.shared.BaseCrudController;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.CrossOrigin;
import org.springframework.web.bind.annotation.RequestMapping;


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
	public Role getnew() {
		return null;
	}

	@Override
	protected Role createInputToEntity(Role role) {
		return role;
	}

	@Override
	protected Role updateInputToEntity(Role role) {
		return role;
	}
}
