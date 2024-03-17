let userProfileApp;

function bindForms() {
    bindFormSubmit({
        btnId: 'save-user-profile-btn',
        formId: 'user-profile-form',
        methodFn: () => {
            return 'post';
        },
        urlFn: () => {
            return `/user/profile`;
        },
        onSuccessFn: (response) => {
            showSuccess('Success');

            if (response.data) {
                userProfileApp.userProfile.username = response.data.username;
                userProfileApp.userProfile.email = response.data.email;
            }

            userProfileApp.isEditMode = false;
        }
    });
}

$(function () {
    userProfileApp = new Vue({
        el: '#user-profile-app',
        data: {
            userProfile: _get('userProfile'),
            isEditMode: false,
        },
        mounted: function () {
            bindForms();
        },
        methods: {
            turnIntoEditMode: function () {
                this.isEditMode = true;
            }
        }
    });
});