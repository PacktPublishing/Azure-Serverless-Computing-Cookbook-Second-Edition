package com.packtpub;

import java.util.List;

public class Devices {
    private List<Device> devices;

    public List<Device> getDevices() {
        return devices;
    }

    public Devices setList(List<Device> devices) {
        this.devices = devices;
        return this;
    }
}