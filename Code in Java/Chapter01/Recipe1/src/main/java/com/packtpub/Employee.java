package com.packtpub;

public class Employee {
    private String firstName = "";
    private String lastName = "";

    public String getFirstName() {
        return firstName;
    }

    public Employee setFirstName(String firstName) {
        if (firstName != null) {
            this.firstName = firstName.trim();
        }
        else {
            this.firstName = "";
        }
        return this;
    }

    public String getLastName() {
        return lastName;
    }

    public Employee setLastName(String lastName) {
        if (lastName != null) {
            this.lastName = lastName.trim();
        }
        else {
            this.lastName = "";
        }
        return this;
    }
}