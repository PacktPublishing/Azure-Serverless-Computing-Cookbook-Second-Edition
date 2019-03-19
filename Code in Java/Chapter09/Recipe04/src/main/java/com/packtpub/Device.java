package com.packtpub;

public class Device {
    private String type;
    private String brand;
    private String model;

    public String getType() {
        return type;
    }

    public Device setType(String type) {
        this.type = type;
        return this;
    }

    public String getBrand() {
        return brand;
    }

    public Device setBrand(String brand) {
        this.brand = brand;
        return this;
    }

    public String getModel() {
        return model;
    }

    public Device setModel(String model) {
        this.model = model;
        return this;
    }
}