redirectToCheckout = function (sessionId) {
    var stripe = Stripe('pk_test_51JeYAbDWIkUkZl2tjx6XA3kvUprIvYFhmfxLi8m5lWn3hdOnJOg8IAx0tU3iaN7U7ILPclcCuaqY7P1bx0sAM2vE00nx6oiJgS');
    stripe.redirectToCheckout({
        sessionId: sessionId
    });
};