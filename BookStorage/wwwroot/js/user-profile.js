let userProfileApp;


$(function () {
    userProfileApp = new Vue({
        el: '#user-profile-app',
        data: {
            userProfile: _get('userProfile'),
            isEditMode: false,
        },
        mounted: function () {
            $('#username').val(this.userProfile.username);
            $('#email').val(this.userProfile.email);

        },
        methods: {
            turnIntoEditMode: function () {
                this.isEditMode = true;
            }
        }
    });
});