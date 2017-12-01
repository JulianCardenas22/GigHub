var GigDetailsController = function (followingServices) {
    var followButton;

    var init = function (e) {
        followButton = $(e.target);
        var followeeId = followButton.attr("data-user-id");

        if (followButton.hasClass("btn-default"))
            followingServices.createFollowing(followeeid, done, fail);
        else
            followingServices.deleteFollowing(followeeid, done, fail);
    };


    var done = function () {
        var text = followButton.text() == "Follow" ? "Follwing" : "Follow";

        followButton.toggleClass("btn-info").toggleClass("btn-default").text(text);
    };
    var fail = function () {
        alert("Something goes Wrong! API Follow");
    };


    return {
        init : init
    }

}(FollowingServices);