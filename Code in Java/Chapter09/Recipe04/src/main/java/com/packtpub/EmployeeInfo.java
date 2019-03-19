package com.packtpub;

import com.fasterxml.jackson.annotation.JsonProperty;

public class EmployeeInfo {
    @JsonProperty("firstname")
    private String firstName = "";
    @JsonProperty("lastname")
    private String lastName = "";
    @JsonProperty("email")
    private String emailAddress = "";
    @JsonProperty("devicelist")
    private String deviceList = "";

    public String getFirstName() {
        return firstName;
    }

    public EmployeeInfo setFirstName(String firstName) {
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

    public EmployeeInfo setLastName(String lastName) {
        if (lastName != null) {
            this.lastName = lastName.trim();
        }
        else {
            this.lastName = "";
        }
        return this;
    }

    public String getEmailAddress() {
        return emailAddress;
    }

    public EmployeeInfo setEmailAddress(String emailAddress) {
        if (emailAddress != null) {
            this.emailAddress = emailAddress.trim();
        }
        else {
            this.emailAddress = "";
        }
        return this;
    }

    public String getDeviceList() {
        return deviceList;
    }

    public EmployeeInfo setDeviceList(String deviceList) {
        if (deviceList != null) {
            this.deviceList = deviceList.trim();
        }
        else {
            this.deviceList = "";
        }
        return this;
    }
}