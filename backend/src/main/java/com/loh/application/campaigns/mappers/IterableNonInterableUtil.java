package com.loh.application.campaigns.mappers;

import com.google.gson.Gson;

import java.util.List;


public class IterableNonInterableUtil {

    @FirstElement
    public <T> String first(List<T> in) {
        Gson gson = new Gson();
        return gson.toJson(in);
    }

    @LastElement
    public <T> T last( List<T> in ) {
        if ( in != null && !in.isEmpty() ) {
            return in.get( in.size() - 1 );
        }
        else {
            return null;
        }
    }

}
