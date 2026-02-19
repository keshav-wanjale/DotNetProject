package com.example.webapp.service;

import com.example.webapp.model.User;
import java.util.List;
import java.util.UUID;

public interface IUserService {
    User addUser(String name, int age);
    List<User> getAllUsers();
    User getUser(UUID id);
    void remove(UUID id);
}
