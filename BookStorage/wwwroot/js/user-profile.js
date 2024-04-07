let userProfileApp;
let avatarFilepond;

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

function setupAvatarFilepond() {
    FilePond.registerPlugin(
        FilePondPluginFileValidateType,
        FilePondPluginImageExifOrientation,
        FilePondPluginImagePreview,
        FilePondPluginImageCrop,
        FilePondPluginImageResize,
        FilePondPluginImageTransform,
        FilePondPluginImageEdit
    );

    avatarFilepond = FilePond.create(
        document.getElementById('avatar-filepond'),
        {
            labelIdle: `Drag & Drop your picture or <span class="filepond--label-action">Browse</span>`,
            imagePreviewHeight: 170,
            imageCropAspectRatio: '1:1',
            imageResizeTargetWidth: 200,
            imageResizeTargetHeight: 200,
            stylePanelLayout: 'compact circle',
            styleLoadIndicatorPosition: 'center bottom',
            styleProgressIndicatorPosition: 'right bottom',
            styleButtonRemoveItemPosition: 'left bottom',
            styleButtonProcessItemPosition: 'right bottom',
        }
    );

    avatarFilepond.addFile(userProfileApp.userProfile.avatarUrl)
        .then(() => {

        })
        .catch(() => {

        })
        .finally(() => {
            avatarFilepond.onaddfile = function () {
                let file = avatarFilepond.getFiles()[0].file;
                if (!!file) {
                    let formData = new FormData();
                    formData.append('files[0]', file);
                    axios
                        .post('/user/avatar', formData)
                        .then((response) => {
                            showSuccess('Avatar updated successfully');
                        })
                        .catch((error) => {

                        });
                }
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
    setupAvatarFilepond();
});