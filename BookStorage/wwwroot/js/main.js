
class FormCfg {
    btnId;
    formId;
    methodFn;
    urlFn;
    onSuccessFn;
    onFailFn;
    successMessage;
    errorMessage;
}

function setBtnLoading(id) {
    if (!!id && id.length > 0 && id.charAt(0) !== '#') {
        id = toId(id);
    }

    $(id).addClass('loading');
}

function removeBtnLoading(id) {
    if (!!id && id.length > 0 && id.charAt(0) !== '#') {
        id = toId(id);
    }

    $(id).removeClass('loading');
}

function toId(word) {
    if (!word || word.length === 0 || word.charAt(0) === "#") {
        return word;
    }

    return "#" + word;
}

function bindFormSubmit(cfg) {
    let submitBtn = $(toId(cfg.btnId));

    submitBtn.on('click', () => {
        handleFormSubmit(cfg);
    });
}

function handleFormSubmit(cfg) {
    setBtnLoading(cfg.btnId);

    if (!cfg.formId) {
        removeBtnLoading(cfg.btnId);
        return;
    }

    let form = $(toId(cfg.formId));

    let isFormValid = form.form('is valid');

    if (!isFormValid) {
        form.form('validate form');
        removeBtnLoading(cfg.btnId);
        return;
    }

    let formData = new FormData(form[0]);
    let requestFnPromise;

    if (!!cfg.methodFn && cfg.methodFn() === 'post') {
        requestFnPromise = axios.post(cfg.urlFn(), formData);
    }
    else if (!!cfg.methodFn && cfg.methodFn() === 'put') {
        requestFnPromise = axios.put(cfg.urlFn(), formData);
    }

    if (!!requestFnPromise) {
        requestFnPromise
            .then((response) => {
                if (!!cfg.successMessage && cfg.successMessage.length > 0) {
                    showSuccess(cfg.successMessage);
                }

                if (!!cfg.onSuccessFn) {
                    cfg.onSuccessFn(response);
                }

                form.form('clear');
            })
            .catch((error) => {
                if (error.response) {
                    handleErrorResponse(error, cfg.errorMessage);
                } else {
                    showError("An unexpected error has occurred. Please try again later.");
                }
            })
            .finally(() => {
                removeBtnLoading(cfg.btnId);
            });
    }
    
}

function handleErrorResponse(error, message) {
    if (!!error && !!error.response && !!error.response.data) {
        let errors = error.response.data.errors;

        if (!!errors && errors.length > 0) {
            for (let i = 0; i < errors.length; i++) {
                showError(errors[i].message);
            }
        }
    }

    if (message !== undefined) {
        showError(message);
    }
}

function showToast(message, toastType) {
    $('body').toast({
        message: message,
        class: toastType,
        displayTime: 5000,
        pauseOnHover: true,
        position: 'bottom left',
    });
}

function showSuccess(message) {
    showToast(message, 'success');
}

function showError(message) {
    showToast(message, 'error');
}

function goTo(url) {
    window.location.href = url;
}

function changePageTitle(newTitle) {
    document.title = `${newTitle} - BookSpot`;
}