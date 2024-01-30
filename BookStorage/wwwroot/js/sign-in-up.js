
function bindForms() {
    bindFormSubmit({
        btnId: 'sign-in-btn',
        formId: 'sign-in-form',
        methodFn: () => {
            return 'post';
        },
        urlFn: () => {
            return '/account/sign-in';
        },
        onSuccessFn: (response) => {
            const searchParams = new URLSearchParams(window.location.search);
            const returnUrl = searchParams.get('returnUrl');

            if (!!returnUrl) {
                goTo(returnUrl);
            } else {
                goTo('/');
            }
        }
    });

    bindFormSubmit({
        btnId: 'sign-up-btn',
        formId: 'sign-up-form',
        methodFn: () => {
            return 'post';
        },
        urlFn: () => {
            return '/account/sign-up';
        },
        onSuccessFn: (response) => {
            $('.menu .item').tab('change tab', 'sign-in');
        },
        successMessage: 'Successful created a new account. Now, sign in.'
    });
}

function setupFormValidation() {
    $('#sign-in-form').form({
        fields: {
            email: {
                identifier: 'email',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your e-mail'
                    },
                    {
                        type: 'email',
                        prompt: 'Please enter a valid e-mail'
                    }
                ]
            },
            password: {
                identifier: 'password',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your password'
                    }
                ]
            },
        }
    });


    $('#sign-up-form').form({
        fields: {
            username: {
                identifier: 'username',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your username'
                    }
                ]
            },
            email: {
                identifier: 'email',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your e-mail'
                    },
                    {
                        type: 'email',
                        prompt: 'Please enter a valid e-mail'
                    }
                ]
            },
            password: {
                identifier: 'password',
                rules: [
                    {
                        type: 'empty',
                        prompt: 'Please enter your password'
                    }
                ]
            },
            confirmPassword: {
                identifier: 'confirm-password',
                rules: [
                    {
                        type: 'match[password]',
                        prompt: 'Passwords don\'t match'
                    },
                    {
                        type: 'empty',
                        prompt: 'Please enter value to confirm your password'
                    }
                ]
            }
        }
    });
}

function setupTabs() {
    $('.menu .item').tab({
        history: true,
        onVisible: (tabPath) => {
            if (!!tabPath && tabPath === 'sign-in') {
                changePageTitle('Sign in');
            }
            else if (!!tabPath && tabPath === 'sign-up') {
                changePageTitle('Sign up');
            }
        }
    });
}

$(function () {
    setupTabs();
    bindForms();
    setupFormValidation();
});
