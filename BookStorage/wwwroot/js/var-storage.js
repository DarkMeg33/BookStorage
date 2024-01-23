let storage = {

}

function _get(key) {
    let value = storage[key];

    return value;
    /*return value ? JSON.parse(JSON.stringify(value)) : value;*/
}

function _set(key, value) {
    storage[key] = value;
}