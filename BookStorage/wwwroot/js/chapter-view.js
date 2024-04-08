let chapterApp;

function bindForms() {
    bindFormSubmit({
        btnId: 'save-user-settings-btn',
        formId: 'user-reading-settings-form',
        methodFn: () => {
            return 'post';
        },
        urlFn: () => {
            return `/user/${chapterApp.userId}/reading-settings`;
        },
        onSuccessFn: (response) => {
            if (response.data) {
                chapterApp.userReadingSettings.userReadingSettingsId = response.data.userReadingSettingsId;
                chapterApp.userReadingSettings.themeMode = response.data.themeMode;
                chapterApp.userReadingSettings.fontSize = response.data.fontSize;
            }

            showSuccess('Success');
        }
    });
}

function setupChaptersDropdown() {
    $('#chapters-nav-dropdown').dropdown({

    });
}

$(function () {
    setupChaptersDropdown();

    chapterApp = new Vue({
        el: '#chapter-app',
        data: {
            userId: _get('userId'),
            userReadingSettings: _get('userReadingSettings'),
            chapter: _get('chapter'),
            bookId: _get('bookId'),
            previousChapterId: _get('previousChapterId'),
            nextChapterId: _get('nextChapterId'),
            isFirstChapter: _get('isFirstChapter'),
            isLastChapter: _get('isLastChapter'),
        },
        mounted: function () {
            $('#user-reading-settings-popup').popup({
                hoverable: true,
                position: 'bottom center',
                inline: true
            });

            $('#user-reading-settings-id-input').val(this.userReadingSettings.userReadingSettingsId);
            $('#theme-mode-input').val(this.userReadingSettings.themeMode);
            $('#font-size-input').val(this.userReadingSettings.fontSize);

            bindForms();
        },
        methods: {
            goToChapter: function (bookId, chapterId) {
                goTo(`/book/${bookId}/chapter/${chapterId}/view`);
            }
        }
    });
});
