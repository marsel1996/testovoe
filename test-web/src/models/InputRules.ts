import { Rule } from "antd/lib/form";

export const requiredRule: Rule = {required: true, message: 'Необходимо заполнить поле'};

export const emailRule: Rule = {
    pattern: /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/,
    message: 'Введены некорректные данные'};

export const phoneRule: Rule = {
    pattern: /^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$/im,
    message: 'Введены некорректные данные'
};