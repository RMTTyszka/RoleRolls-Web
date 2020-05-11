package com.loh.utils;

import java.io.*;

public class ObjectConverter<T> {
    public byte[] serialize(T obj) throws IOException {

        ByteArrayOutputStream bos = new ByteArrayOutputStream();
        ObjectOutputStream oos = new ObjectOutputStream(bos);
        oos.writeObject(obj);
        oos.flush();
        byte [] data = bos.toByteArray();
        return data;
    }
    public T deserialize(byte[] data) throws IOException, ClassNotFoundException {
        if (data == null) {
            return null;
        }
        ByteArrayInputStream in = new ByteArrayInputStream(data);
        ObjectInputStream is = new ObjectInputStream(in);
        return (T)is.readObject();
    }
}
