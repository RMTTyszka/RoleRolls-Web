package com.rolerolls.domain.contexts;

import lombok.Getter;
import lombok.Setter;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import java.util.UUID;

@Entity
public class Player {

	@Getter
	@Setter
	private String name;

	@Id
	@Getter
	@Setter
	@Column(columnDefinition = "BINARY(16)")
	protected UUID id;

	public Player() {
		
	}
	public Player(UUID id, String name) {
		this.name = name;
		this.id = id;
	}

}
