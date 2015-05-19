package com.readmore.tonka.helpers;

import java.io.Serializable;

/**
 * Created by anton on 18.05.2015..
 */
public abstract class MyRunnable<T> implements Serializable
{
    public abstract void run(T response);
}