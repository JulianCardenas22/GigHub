var GigDetailsController = function (followingServices) {
    var followButton;

    var init = function (container) {
     $(".js-toggle-follow").click(toggleFollowing);
    };

    var toggleFollowing = function (e) {
        followButton = $(e.target);

        var followeeId = followButton.attr("data-user-id");

        if (followButton.hasClass("btn-default"))
            followingServices.createFollowing(followeeId, done, fail);
        else
            followingServices.deleteFollowing(followeeId, done, fail);
    };


    var done = function () {
        var text = followButton.text() == "Follow" ? "UnFollow" : "Follow";

        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };
    var fail = function () {
        alert("Something goes Wrong! API Follow");
    };


    return {
        init: init
    }

}(FollowingServices);